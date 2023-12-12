// RegistrationForm.tsx
import React, { useState } from 'react';
import { Form, Input, Button, Select, DatePicker } from 'antd';
import 'bootstrap/dist/css/bootstrap.min.css';
import StudentModel from '../Models/StudentModel';
import '../Styles/registration.css';

const { Option } = Select;

interface RegistrationFormProps {
  handleRegistration: (student: StudentModel) => void;
}

const RegistrationForm: React.FC<RegistrationFormProps> = ({ handleRegistration }) => {

    const [form] = Form.useForm();
    const [selectedTechStack, setSelectedTechStack] = useState<string[]>([]);
  
    const handleTechStackChange = (value: string[]) => {
      setSelectedTechStack(value);
    };

  const onFinish = (values: any) => {
    const student: StudentModel = {
      Id: Math.floor(Math.random() * 1000),
      TechStack: selectedTechStack,
      ...values,
    };
    handleRegistration(student);
  };

  return (
    
    <Form form={form} onFinish={onFinish} labelCol={{ span: 6 }} wrapperCol={{ span: 16 }} justify-content-center flex-wrap>
      <Form.Item label="Name" name="Name" rules={[{ required: true, message: 'Please enter your name' }]}>
        <Input />
      </Form.Item>

      <Form.Item
        label="Mail Address"
        name="MailAddress"
        rules={[{ required: true, message: 'Please enter your email address' }]}
      >
        <Input type="email" />
      </Form.Item>

      <Form.Item label="Password" name="Password" rules={[{ required: true, message: 'Please enter your password' }]}>
        <Input.Password />
      </Form.Item>

      <Form.Item label="Phone Number" name="PhoneNumber">
        <Input />
      </Form.Item>

      
         <Form.Item label="Tech Stack" name="TechStack">
            <Select mode="multiple" placeholder="Select tech stack" onChange={handleTechStackChange}>
            <Option value="React">React</Option>
            <Option value="Node.js">Node.js</Option>
            <Option value="C#">C#</Option>
            <Option value="Java">Java</Option>
            <Option value="WebDev">WebDev</Option>
            <Option value="C++">C++</Option>
            <Option value="Python">Python</Option>
            <Option value="MySQL">MySQL</Option>
            <Option value="Go">Go</Option>
            <Option value="TypeScript">TypeScript</Option>
            <Option value="Unity">Unity</Option>
            <Option value="Angular">Angular</Option>
            <Option value="ASP.NET">ASP.NET</Option>
            <Option value="JavaScript">JavaScript</Option>
            <Option value=".NetFramework">.Net Framework</Option>
            <Option value="AndroidDevelopment">Android Development</Option>
          
            </Select>
         </Form.Item>
      
         <Form.Item label="Graduation" className='mx-auto' name="Graduation">
        <DatePicker />
      </Form.Item>

      <Form.Item label="Start Lia 1" name="StartLia1">
        <DatePicker />
      </Form.Item>

      <Form.Item label="End Lia 1" name="EndLia1">
        <DatePicker />
      </Form.Item>

      <Form.Item label="Start Lia 2" name="StartLia2">
        <DatePicker />
      </Form.Item>

      <Form.Item label="End Lia 2" name="EndLia2">
        <DatePicker />
      </Form.Item>

      <Form.Item label="Presentation" name="Presentation">
        <Input.TextArea />
      </Form.Item>

      <Form.Item label="Image" name="ImageUrl">
        <Input />
      </Form.Item>

      <Form.Item label="LinkedIn" name="LinkedInProfile">
        <Input />
      </Form.Item>

      <Form.Item wrapperCol={{ offset: 6, span: 16 }}>
        <Button type="primary" htmlType="submit">
          Register
        </Button>
      </Form.Item>
   
    </Form>
  );
};

export default RegistrationForm;
