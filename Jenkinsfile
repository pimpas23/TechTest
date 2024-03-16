pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
				echo "building..."
                sh "dotnet build TechTest.sln -c Release"
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
				sh "unit-tests"
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}