from repositories.repository import Repository
from models.person import Person
from playhouse.shortcuts import model_to_dict
from enums.reponse_enum import HttpResponseError


class CustomError(Exception):
    def __init__(self, message="Custom exception"):
        self.message = message
        super().__init__(self.message)

class PersonRepository (Repository):
    def __init__(self, person: Person):
        self.person = person

    def get_all(self):
        return list(self.person.select().dicts())

    def get_by_id(self, id):
        query = self.person.get_or_none(Person.id == id)
        print(query)
        if query is None:
            return None
        get_element = self.person.get_by_id(id)
        json_response = model_to_dict(get_element)
        return json_response

    def create(self, json_recibed):
        if not json_recibed or "name" not in json_recibed or "age" not in json_recibed:
            return HttpResponseError.BAD_REQUEST
        new_entity = self.person.create(name=json_recibed["name"], age=json_recibed["age"])
        json_response = model_to_dict(new_entity)
        return json_response

    def update(self, id, json_recibed):
        if not json_recibed or "name" not in json_recibed or "age" not in json_recibed:
            return HttpResponseError.BAD_REQUEST
        query = self.person.get_or_none(Person.id == id)
        if query is None:
            return None
        get_element = self.person.get_by_id(id).get()
        get_element.name = json_recibed["name"]
        get_element.age = json_recibed["age"]
        return get_element.save()

    def delete(self, id):
        query = self.person.get_or_none(Person.id == id)
        if query is None:
            return None
        get_element = self.person.get_by_id(id)
        return get_element.delete_instance()

