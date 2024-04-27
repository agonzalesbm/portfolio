from services.service import Service
from repositories.person_repository import PersonRepository

class PersonService(Service):
    def __init__(self, repo: PersonRepository):
        self.repo = repo

    def get_all(self):
        return self.repo.get_all()

    def get_by_id(self, id):
        return self.repo.get_by_id(id)

    def create(self, entity):
        return self.repo.create(entity)

    def update(self, id, entity):
        return self.repo.update(id, entity)

    def delete(self, id):
        return self.repo.delete(id)
