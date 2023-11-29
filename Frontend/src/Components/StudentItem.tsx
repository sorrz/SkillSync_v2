// StudentItem.tsx
import React from 'react';
import { Card, Avatar, Row, Col } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import StudentModel from '../Models/StudentModel';

interface StudentItemProps {
  student: StudentModel;
}

const StudentItem: React.FC<StudentItemProps> = ({ student }) => {
  return (
    <Card className="mb-4 border rounded shadow">
      <Row justify="space-between" align="middle">
        <Col span={4}>
          {/* Profile picture */}
          <Avatar size={80} icon={<UserOutlined />} src={student.ImageUrl} className="rounded" />
        </Col>
        <Col span={16}>
          {/* Name and TechStack */}
          <div className="text-center" style={{ background: '#ccffcc', padding: '10px', borderRadius: '8px' }}>
            <h3>{student.Name}</h3>
            <p>{student.TechStack.join(', ')}</p>
          </div>
        </Col>
      </Row>

      {/* Presentation */}
      <Card.Meta title="Presentation" description={<p>{student.Presentation}</p>} style={{ marginTop: '10px' }} />

      {/* Additional information at the bottom */}
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
