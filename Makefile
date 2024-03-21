.PHONY: build clean infra unit-tests dependencies integration-tests

default-network := techtest-network 

network:
	@docker network ls | grep ${default-network} > /dev/null || docker network create --driver bridge ${default-network}

infra: network
	@docker-compose up -d sql_server
	@docker cp ./scripts/create_database.sql techtest_sql_server_1:/create_database.sql
	@docker exec -it techtest_sql_server_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P TechTest112358.# -d master -i /create_database.sql
	@docker-compose up -d --build

run: infra
	@docker-compose -f docker-compose.yaml up -d app

dependencies: network
	@docker-compose up -d sql_server
	@docker cp ./scripts/create_database.sql techtest_sql_server_1:/create_database.sql
	@docker exec -it techtest_sql_server_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P TechTest112358.# -d master -i /create_database.sql

unit-tests:
	@docker-compose -f docker-compose.yaml up --abort-on-container-exit --exit-code-from unit-tests unit-tests
	
integration-tests:
	@docker-compose -f docker-compose.yaml up --abort-on-container-exit --exit-code-from integration-tests integration-tests
	
create-db:
	@docker-compose up -d sql_server
	@docker cp ./scripts/create_database_integration_tests.sql techtest_sql_server_1:/create_database_integration_tests.sql
	@docker exec -it techtest_sql_server_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P TechTest112358.# -d master -i /create_database_integration_tests.sql

clean:
	@docker-compose -f docker-compose.yaml down