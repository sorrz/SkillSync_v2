// Home.tsx

import { Row, Col, Card, Typography } from "antd";
import { Container } from "react-bootstrap";
import "../Styles/homes.css"

const { Title, Paragraph } = Typography;

export function Home(){
  return (
    <>
      {/* Block 1 - Hero Banner */}
      <div className="hero-banner">
        <Container>
          <Title level={1}>Hero Banner Title</Title>
          <Paragraph>Some text over the picture</Paragraph>
        </Container>
      </div>

      {/* Block 2 - Grey Background with Responsive and Centered Cards */}
      <div className="grey-background">
        <Container>
          <Row gutter={[16, 16]} justify="center">
            {/* Adjust the number of Col components based on your needs */}
            <Col xs={24} sm={12} md={8} lg={6}>
              <Card className="custom-card" title="Card 1">
                Content for Card 1
              </Card>
            </Col>
            <Col xs={24} sm={12} md={8} lg={6}>
              <Card className="custom-card" title="Card 2">
                Content for Card 2
              </Card>
            </Col>
            <Col xs={24} sm={12} md={8} lg={6}>
              <Card className="custom-card" title="Card 3">
                Content for Card 3
              </Card>
            </Col>
            {/* Add more cards as needed */}
          </Row>
        </Container>
      </div>

      {/* Block 3 - Grey/Green Pastel Background with Huge Title and Paragraphs */}
      <div className="grey-green-background">
        <Container>
          <Title level={2}>Huge Title</Title>
          <Paragraph>First paragraph of text</Paragraph>
          <Paragraph>Second paragraph of text</Paragraph>
        </Container>
      </div>
    </>
  );}