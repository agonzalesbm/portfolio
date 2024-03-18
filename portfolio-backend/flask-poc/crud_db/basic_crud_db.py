from flask import Flask, request, jsonify, abort
from peewee import PostgresqlDatabase
from peewee import Model
from peewee import TextField
from peewee import IntegerField
from peewee import SQL
from playhouse.shortcuts import model_to_dict

db = PostgresqlDatabase('learnflask', host='localhost',
                        port=5432, user='postgres', password='password')


class MyUser (Model):
    id = IntegerField(primary_key=True)
    name = TextField()
    city = TextField(constraints=[SQL("DEFAULT 'Mumbai'")])
    age = IntegerField()

    class Meta:
        database = db
        # if db_table is not specify the table name will be lowercase class name
        # db_table = 'MyUser'


db.connect()
db.create_tables([MyUser])


app = Flask(__name__)


@app.post('/')
def create_user():
    json_recibed = request.get_json()
    if not json_recibed or 'name' not in json_recibed:
        return 'bad re', 400
    new_user = MyUser(
        name=json_recibed['name'], city=json_recibed['city'], age=json_recibed['age'])
    new_user.save()
    json_data = model_to_dict(new_user)
    return jsonify(json_data), 201


@app.get('/')
def get_all():
    users = list(MyUser.select().dicts())
    return jsonify(users)


@app.get('/<int:id>')
def get_by_id(id):
    query = MyUser.get_or_none(MyUser.id == id)
    if query is None:
        return "Resource not found", 404
    print(query)
    json_data = model_to_dict(query)
    print(json_data)
    return jsonify(json_data)
