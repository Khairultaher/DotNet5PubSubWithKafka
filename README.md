# DotNet5PubSubWithKafka
# Install JRE and set environment variable path to system variable section
JAVA_HOME=C:\java
then add it to path section like following
%JAVA_HOME%\bin 
open cmd and run following commands to ensure version and path
For version: java -version
For path: where java

# Change zookeeper.properties
dataDir=c:/kafka/zookeeper-data

# Change server.properties 
log.dirs=c:/kafka/kafka-log

# Run Zookeeper
Go to kafka directory and run following command 
.\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties

# Run Kafka
Go to kafka directory and run following command
.\bin\windows\kafka-server-start.bat .\config\server.properties

# Create topic
.\bin\windows\kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic testTopic

# Check created topic list
.\bin\windows\kafka-topics.bat --list --zookeeper localhost:2181

# Send message
.\bin\windows\kafka-console-producer.bat --broker-list localhost:9092 --topic testTopic

# Read mesage 
.\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic testTopic --from-beginning





