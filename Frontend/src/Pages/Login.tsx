import { useState } from "react";
import { Form, Input, Button, Space } from "antd";
import { LockOutlined, UserOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router";

interface LoginProps {
  onLogin: (email: string, password: string) => void;
}

export function Login({ onLogin }: LoginProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();
  const handleLogin = () => {
    onLogin(email, password);
  };

  const handleRegisterClick = () => {
    navigate('/register');
  };

  return (
    <div className="LoginContainer bg-white shadow-sm rounded p-4" style={{ position: "relative",
    maxWidth: "400px",
    margin: "auto",
    marginTop: "50px",
    background: "url('../assets/dans_logo_png.png') no-repeat center center",
    backgroundSize: "cover",
    padding: "20px" }}>
      <h3>Login</h3>
      <p>Please sign in with your email and password:</p>
      <Form onFinish={handleLogin} initialValues={{ remember: true }} size="large">
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
      <div className="mt-3 text-center">
        <p>
          Don't have an account? 
          <span onClick={handleRegisterClick} style={{ cursor: "pointer", color: "blue" }}>
            Register here
          </span>
        </p>
      </div>
    </div>
  );
}
