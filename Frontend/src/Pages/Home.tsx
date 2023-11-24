import { Row, Col, Card, Typography } from "antd";
import { Container } from "react-bootstrap";
import "../Styles/homes.css";
import heroBannerImage from "../assets/herobanner.jpg"; // Replace with the actual path to your image

const { Title, Paragraph } = Typography;

export function Home() {
  return (
    <>
      <div className="hero-banner" style={{ color: "white", height: "50vh", backgroundImage: `url(${heroBannerImage})` }}>
        <Container>
          <Title level={1}>Hero Banner Title</Title>
          <Paragraph>Some text over the picture</Paragraph>
        </Container>
      </div>

      <div className="grey-background" style={{ height: "90vh" }}>
        <Container>
          <Row gutter={[16, 16]} justify="center">
            <Col xs={24} sm={12} md={8} lg={6}>
              <Card className="custom-card" style={{ height: "40vh" }} title="Card 1">
                Content for Card 1
              </Card>
            </Col>
            <Col xs={24} sm={12} md={8} lg={6}>
              <Card className="custom-card" style={{ height: "40vh" }} title="Card 2">
                Content for Card 2
              </Card>
            </Col>
            <Col xs={24} sm={12} md={8} lg={6}>
              <Card className="custom-card" style={{ height: "40vh" }} title="Card 3">
                Content for Card 3
              </Card>
            </Col>
          </Row>
        </Container>
      </div>

      <div className="grey-green-background" style={{ height: "90vh" }}>
        <Container>
          <Title level={2}>Third Title</Title>
          <Paragraph>First paragraph of text</Paragraph>
          <Paragraph>Second paragraph of text</Paragraph>
        </Container>
      </div>
    </>
  );
}
