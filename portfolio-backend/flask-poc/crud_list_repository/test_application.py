import unittest
from services.person_service import PersonService
from repositories.person_repository import PersonRepository
from models.person import Person


class TestStringMethods(unittest.TestCase):
    def setUp(self):
        self.person = Person()
        self.mock_repository = PersonRepository(self.person)
        self.person_service = PersonService(self.mock_repository)

    def test_create_person(self):
        json_sample = {"name": "Gabriela", "age": 45}
        person = self.person_service.create(json_sample)
        self.assertIsNotNone(person)
        self.assertEqual(person["name"], "Gabriela")
        self.assertEqual(person["age"], 45)
        self.assertTrue(self.mock_repository.create)

    def test_update_person(self):
        json_sample = {"name": "Mario", "age": 30}
        self.mock_repository.update(20, json_sample)
        result = self.person_service.update(20, json_sample)
        self.assertIsNone(result)
        self.assertTrue(self.mock_repository.update)

    def test_delete_person(self):
        self.mock_repository.delete(5)
        result = self.person_service.delete(5)
        self.assertIsNone(result)
        self.assertTrue(self.mock_repository.delete)


if __name__ == "__main__":
    unittest.main()
