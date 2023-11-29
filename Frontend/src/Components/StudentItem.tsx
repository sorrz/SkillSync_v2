import React from 'react';
import { Card, Avatar, Row, Col } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import StudentModel from '../Models/StudentModel';

interface StudentItemProps {
  student: StudentModel;
}

const StudentItem: React.FC<StudentItemProps> = ({ student }) => {
  return (
    <Card className="mb-4 border rounded shadow" data-testid="studentCard">
      <Row justify="space-between" align="middle">
        <Col span={4}>
          <Avatar size={150} icon={<UserOutlined />} src={student.ImageUrl} className="circle" />
        </Col>
        <Col span={16}>
          <div className="text-center" style={{ background: '#ccffcc', padding: '10px', borderRadius: '8px' }}>
            <h3>{student.Name}</h3>
            <p>{student.TechStack.join(', ')}</p>
          </div>
        </Col>
      </Row>

      <Card.Meta title="About me" description={<p>{student.Presentation}</p>} style={{ marginTop: '10px' }} />

      <div className="pt-4 mt-auto text-muted text-center">
        <p>
          Phone: {student.PhoneNumber} | Graduation: {student.Graduation} | 
          LIA 1: {student.StartLia1} - {student.EndLia1} | 
          LIA 2: {student.StartLia2} - {student.EndLia2}
        </p>
      </div>
    </Card>
  );
};

export default StudentItem;
