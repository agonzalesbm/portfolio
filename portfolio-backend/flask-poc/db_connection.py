from peewee import PostgresqlDatabase
from peewee import Model
from peewee import TextField
from peewee import IntegerField
from peewee import SQL

db = PostgresqlDatabase('learnflask', host='localhost',
                        port=5432, user='postgres', password='password')


class MyUser (Model):
    name = TextField()
    city = TextField(constraints=[SQL("DEFAULT 'Mumbai'")])
    age = IntegerField()

    class Meta:
        database = db
        db_table = 'MyUser'


db.connect()
db.create_tables([MyUser])
