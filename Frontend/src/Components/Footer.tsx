import { Container } from "react-bootstrap";

const Footer = () => {
  return (
    <footer className="footer mt-auto py-3" data-testid="footer-id">
      <Container className="bg-light" data-testid="footer-container">
        <p>© {new Date().getFullYear()} Your Company Name</p>
      </Container>
    </footer>
  );
};

export default Footer;
