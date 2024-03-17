from flask import Flask, request, jsonify

app = Flask(__name__)

items = [{"id": 1, "name": "Andy"}, {"id": 2, "name": "Name"}]


def find_by_id(id):
    for item in items:
        if item['id'] == id:
            return item
    return None


@app.route('/', methods=['GET'])
def get_all():
    return jsonify(items)


@app.route('/<int:id>', methods=['GET'])
def get_by_id(id):
    item = find_by_id(id)
    print(id)
    if item:
        return jsonify(item)
    else:
        return jsonify({"Error:": "Not found"}), 404


@app.route('/', methods=['POST'])
def create_item():
    new_object = request.get_json()

    if not new_object or 'name' not in new_object:
        return jsonify({"Error": "Invalid json data"}), 400

    new_object['id'] = len(items) + 1
    items.append(new_object)
    return jsonify(new_object), 201


@app.route('/<int:id>', methods=['DELETE'])
def delete_item(id):
    item = find_by_id(id)

    if not item:
        return jsonify({"Error": "Not found"}), 404

    items.remove(item)
    return jsonify({"Message": "Object deleted"}), 200


@app.route('/<int:id>', methods=['PUT'])
def update_item(id):
    item = find_by_id(id)

    if not item:
        return jsonify({"Error": "Not found"}), 404

    update_item = request.get_json()

    if not update_item or 'name' not in update_item:
        return jsonify({"Error": "Ivalid json data"}), 400

    item.update(update_item)
    return jsonify(update_item), 200


if __name__ == '__main__':
    app.run(debug=True)
