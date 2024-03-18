from peewee import Model
from database.db_connection import db

class BaseModel(Model):
    class Meta:
        database = db
