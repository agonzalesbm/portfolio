from peewee import AutoField, IntegerField, TextField
from models.base_model import BaseModel

class Person(BaseModel):
    # id=IntegerField(primary_key=True)
    id=AutoField(primary_key=True)
    name=TextField()
    age=IntegerField()

