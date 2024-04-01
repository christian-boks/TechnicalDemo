using Testcontainers.PostgreSql;

namespace DbInit;

public static class DbInit
{
    public static async Task Setup(PostgreSqlContainer postgres)
    {
        var sql = """
            CREATE TABLE salesperson (
                id SERIAL PRIMARY KEY, 
                name VARCHAR(100) NOT NULL
            );

            CREATE TABLE district (
                id SERIAL PRIMARY KEY, 
                name VARCHAR(100) NOT NULL, 
                primary_salesperson_id INTEGER NOT NULL REFERENCES salesperson
            );

            CREATE TABLE store (
                id SERIAL PRIMARY KEY, 
                city VARCHAR(100) NOT NULL, 
                district_id INTEGER NOT NULL REFERENCES district
            );

            CREATE TABLE district_salesperson (
                salesperson_id integer REFERENCES salesperson(id), 
                district_id integer REFERENCES district(id),
                PRIMARY KEY (salesperson_id, district_id)
            );
            
            INSERT INTO salesperson (id, name)
            VALUES (1,'Kurt');

            INSERT INTO salesperson (id, name)
            VALUES (2,'Hans');

            INSERT INTO salesperson (id, name)
            VALUES (3,'Frank');

            INSERT INTO district (id, name, primary_salesperson_id)
            VALUES (1, 'Northern Denmark', 1);

            INSERT INTO district (id, name, primary_salesperson_id)
            VALUES (2, 'Southern Denmark', 2);

            INSERT INTO district (id, name, primary_salesperson_id)
            VALUES (3, 'Eastern Denmark', 2);

            INSERT INTO store (id, city, district_id)
            VALUES (1, 'Aalborg', 1);

            INSERT INTO store (id, city, district_id)
            VALUES (2, 'Esbjerg', 2);

            INSERT INTO store (id, city, district_id)
            VALUES (3, 'Copenhagen', 3);

            INSERT INTO district_salesperson (salesperson_id,district_id) VALUES (3,1);
        """;

        await postgres.ExecScriptAsync(sql);
    }

}
