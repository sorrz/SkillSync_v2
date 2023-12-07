import { useEffect, useState } from 'react';
import { Container, Col } from 'react-bootstrap';
import { getAllStudents } from '../Services/CompanyServices';
import StudentItem from '../Components/StudentItem';
import StudentModel from '../Models/StudentModel';

export function Students() {
  const [students, setStudents] = useState<StudentModel[]>([]);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const studentsData = await getAllStudents();
        setStudents(studentsData);
      } catch (error) {
        console.error('Error fetching students:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <Container>
      <h1>Students</h1>
      {students.length > 0 ? (
        <Container>
          {students.map((student) => (
            <Col md={4} key={student.id} style={{ marginBottom: '16px' }}>
              <StudentItem student={student} />
            </Col>
          ))}
        </Container>
      ) : (
        <p>No students to list.</p>
      )}
    </Container>
  );
}
