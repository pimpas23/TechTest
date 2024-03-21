.PHONY: build clean infra run unit-tests integration-tests create-db network

default-network := techtest-network 

network:
	@docker network ls | grep ${default-network} > /dev/null || docker network create --driver bridge ${default-network}

infra: network
	@docker-compose up -d sql_server
	@docker cp ./scripts/create_database.sql techtest_sql_server_1:/create_database.sql
	@docker exec -it techtest_sql_server_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P TechTest112358.# -d master -i /create_database.sql
	@docker-compose up -d --build

run: infra
	@docker-compose up -d app

dependencies: network
	@docker-compose up -d sql_server
	@docker cp ./scripts/create_database.sql techtest_sql_server_1:/create_database.sql
	@docker exec -it techtest_sql_server_1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P TechTest112358.# -d master -i /create_database.sql

unit-tests: network
	@docker-compose -f docker-compose.yaml up --abort-on-container-exit unit-tests

integration-tests: network
	@docker-compose -f docker-compose.yaml up --abort-on-container-exit integration-tests

create-db: network
	@docker-compose up -d create-db
	@docker cp ./scripts/create_database_integration_tests.sql integration_db:/create_database_integration_tests.sql
	@docker exec -it integration_db /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P TechTest112358.# -d master -i /create_database_integration_tests.sql

clean:
	@docker-compose -f docker-compose.yaml down
