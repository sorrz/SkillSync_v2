import { Container } from "react-bootstrap";

const Footer = () => {
  return (
    <footer className="footer mt-auto py-3">
      <Container className="bg-light">
        <p>© {new Date().getFullYear()} Your Company Name</p>
      </Container>
    </footer>
  );
};

export default Footer;
