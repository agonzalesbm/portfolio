from abc import ABC, abstractmethod

class Service(ABC):
    @abstractmethod
    def get_all(self):
        pass

    @abstractmethod
    def get_by_id(self, id):
        pass

    @abstractmethod
    def create(self, entity):
        pass

    @abstractmethod
    def update(self, id, entity):
        pass

    @abstractmethod
    def delete(self, id):
        pass
