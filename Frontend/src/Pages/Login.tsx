import { useState } from "react";
import { Form, Input, Button, Space } from "antd";
import { LockOutlined, UserOutlined } from "@ant-design/icons";

interface LoginProps {
  onLogin: (email: string, password: string) => void;
}

export function Login({ onLogin }: LoginProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = () => {
    onLogin(email, password);
  };


  return (
      <div className="LoginContainer bg-white shadow-sm rounded">
      <h3>Login</h3>
      <p>please sign-in with your e-mail and password:</p>
      <Form
        onFinish={handleLogin}
        initialValues={{ remember: true }}
        size="large"
      >
        <Form.Item
          name="email"
          rules={[{ required: true, message: "Please input your email!" }]}
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
          rules={[{ required: true, message: "Please input your password!" }]}
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
