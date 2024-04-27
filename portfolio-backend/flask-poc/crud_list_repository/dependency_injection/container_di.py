from dependency_injector import containers, providers
from peewee import PostgresqlDatabase
from models.person import Person
from repositories.person_repository import PersonRepository
from services.person_service import PersonService

class Container (containers.DeclarativeContainer):
    person = providers.Factory(Person)

    person_repo = providers.Factory(PersonRepository, person=person)

    person_service = providers.Factory(PersonService, repo=person_repo)

