import StudentItem from '../Components/StudentItem';
import students from '../Data/students.json';

export function Students() {
  return (
    <>
      <h1>Students</h1>
      {students.length > 0 ? (
        <div>
          {students.map((student) => (
            <div key={student.Id} style={{ marginBottom: '16px' }}>
              <StudentItem student={student} />
            </div>
          ))}
        </div>
      ) : (
        <p>No students to list.</p>
      )}
    </>
  );
}
