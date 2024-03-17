from services.person_service import PersonService
from flask import jsonify, request, Blueprint
from dependency_injector.wiring import inject, Provide
from dependency_injection.container_di import Container
from flask import Response, abort
from enums.reponse_enum import HttpResponseError

person_print = Blueprint("person_contoller", __name__)

@person_print.get("/people")
@inject
def get_people(person_service: PersonService = Provide[Container.person_service]):
    response = person_service.get_all()
    return jsonify(response), 200

@person_print.get("/person/<int:id>")
@inject
def get_person_by_id(id, person_service: PersonService = Provide[Container.person_service]):
    response = person_service.get_by_id(id)
    if response is None:
        abort(404, description=f"Person not found id: {id}")
    return jsonify(response), 200

@person_print.post("/person")
@inject
def create_person(person_service: PersonService = Provide[Container.person_service]):
    json_recibed = request.get_json()
    response = person_service.create(json_recibed)
    if response is HttpResponseError.BAD_REQUEST:
        abort(400, description="Json sended should be contain name and age labels")
    return jsonify(response), 201

@person_print.put("/person/<int:id>")
@inject
def update_person(id, person_service: PersonService = Provide[Container.person_service]):
    json_recibed = request.get_json()
    response = person_service.update(id, json_recibed)
    if response is HttpResponseError.BAD_REQUEST:
        abort(400, description="Json sended should be contain name and age labels")
    if response is None:
        abort(404, description=f"Person not found id: {id}")
    return Response(status=204)

@person_print.delete("/person/<int:id>")
@inject
def delete_person(id, person_service: PersonService = Provide[Container.person_service]):
    result = person_service.delete(id)
    if result is None:
        abort(404, description=f"Person not found id: {id}")
    return Response(status=204)

@person_print.errorhandler(404)
def person_not_found(e):
    return jsonify(error=str(e)), 404

@person_print.errorhandler(400)
def bad_request(e):
    return jsonify(error=str(e)), 400
