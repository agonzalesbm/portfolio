from abc import ABC, abstractmethod

class Repository(ABC):
    @abstractmethod
    def get_all(self):
        pass

    @abstractmethod
    def get_by_id(self, id):
        pass

    @abstractmethod
    def create(self, json_recibed):
        pass

    @abstractmethod
    def update(self, id, json_recibed):
        pass

    @abstractmethod
    def delete(self, id):
        pass


