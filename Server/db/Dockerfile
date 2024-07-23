FROM mariadb:latest AS mariadb
RUN touch /var/log/mysql/mysqld.log

ADD  ./db/my.cnf /etc/mysql/conf.d/my.cnf
ADD ./db/sql.sql /docker-entrypoint-initdb.d

RUN chmod 644 /etc/mysql/my.cnf