FROM mysql:5.7

ADD asset/project_management_dump.sql /docker-entrypoint-initdb.d
