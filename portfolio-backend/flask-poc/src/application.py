from flask import Flask
from models.base_model import db
from models.person import Person
from dependency_injection.container_di import Container
from repositories.person_repository import PersonRepository
from controllers.person_controller import person_print

db.connect()
db.create_tables([Person])

app = Flask( __name__)
app.register_blueprint(person_print)

container = Container()
container.wire(modules=[
    "controllers.person_controller"
])

if __name__ == "__main__":
    app.run(debug=True)
