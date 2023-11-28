// Login.tsx

import { useState } from 'react';
import { UserModel } from '../Models/UserModel';
import { Form, Input, Button, Space } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import '../Styles/login.css';
import { useNavigate } from "react-router-dom";



interface LoginProps {
  onLogin: (user: UserModel) => void;
}

export function Login({ onLogin }: LoginProps) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = () => {
    const hashedPassword = hashPassword(password);

    const user: UserModel = {
      mailAdress: email,
      password: hashedPassword,
      name: '',
      TechStack: [],
      phoneNumber: '',
      liaPeriod1: '',
      liaPeriod2: '',
      presentation: '',
      imgURL: ''
    };
    onLogin(user);

    navigate('/profile');
    
    
    // IMPLEMENT HANDLER FOR REDIRECTION TO PROFILE WHEN LOGGED IN
  };

  const hashPassword = (password: string): string => {
    return 'hashed:' + password;
  };

  return (
    <div className="LoginContainer">
      <h3>Login Form</h3>
      <Form onFinish={handleLogin} initialValues={{ remember: true }} size="large">
        <Form.Item
          name="email"
          rules={[{ required: true, message: 'Please input your email!' }]}
        >
          <Input
            prefix={<UserOutlined className="site-form-item-icon" />}
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Item>
        <Form.Item
          name="password"
          rules={[{ required: true, message: 'Please input your password!' }]}
        >
          <Input
            prefix={<LockOutlined className="site-form-item-icon" />}
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Item>
        <Form.Item>
          <Space direction="vertical">
            <Button htmlType="submit" block>
              Log in
            </Button>
          </Space>
        </Form.Item>
      </Form>
    </div>
  );
}
