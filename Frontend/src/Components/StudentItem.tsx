import React, { useEffect } from 'react';
import { Card, Row, Col, Avatar, List } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import StudentModel from '../Models/StudentModel';

interface StudentItemProps {
  student: StudentModel;
}

const StudentItem: React.FC<StudentItemProps> = ({ student }) => {
  useEffect(() => {
    console.log("StudentItem props:", student);
  }, [student]);

  if (!student || !student.name || !student.presentation || !student.phoneNumber) {
    console.error('Invalid student data:', student);
    return null; // or handle accordingly
  }

  return (
    <Card className="mb-4 border rounded shadow" data-testid="studentCard" style={{  width: '65vw', margin: '0.2rem' }}>
      <Row justify="space-between" align="middle">
        <Col span={4}>
          <Avatar size={150} icon={<UserOutlined />} src={student.imageUrl} className="circle" />
        </Col>
        <Col span={16}>
          <div className="text-center" data-testid="name-holder" style={{ background: '#ccffcc', padding: '10px', borderRadius: '5px' }}>
            {student.name && (
              <h3>Hi, I'm <br />{student.name}</h3>
            )}
            {student.techStack && (
              <List
                size="small"
                bordered
                dataSource={student.techStack}
                renderItem={(tech) => <List.Item>{tech}</List.Item>}
                style={{ marginTop: '10px' }}
              />
            )}
          </div>
        </Col>
      </Row>

      <div className="pt-4 mt-auto text-muted text-center">
        <p>
          Phone: {student.phoneNumber} | Graduation: {student.graduation} | 
          LIA 1: {student.startLia1} - {student.endLia1} | 
          LIA 2: {student.startLia1} - {student.endLia2}
        </p>
      </div>

      <div className="card-body">
        <a href={`/profile/${student.id}`} className="card-link">Profile Link</a>
      </div>
    </Card>
  );
};

export default StudentItem;
