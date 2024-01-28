CREATE TABLE IF NOT EXISTS students (
  id SERIAL PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  fathers_name VARCHAR(50) NOT NULL,
  mothers_name VARCHAR(50) NOT NULL,
  born_date DATE
);

CREATE TABLE IF NOT EXISTS subjects (
  id SERIAL PRIMARY KEY,
  name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS students_subjects (
  student_id INTEGER,
  subject_id INTEGER,
  FOREIGN KEY (student_id) REFERENCES students(id),
  FOREIGN KEY (subject_id) REFERENCES subjects(id)
);
