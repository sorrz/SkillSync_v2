import React from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import { CompanyModel } from '../Models/CompanyModel';

interface CompanyItemProps {
  company: CompanyModel
}

const CompanyItem: React.FC<CompanyItemProps> = ( props ) => {
  return (
    <Card className="rounded mb-3 shadow" data-testid="card-id">
      <Card.Img variant="top" src={props.company.imgURL} alt={props.company.name} className="rounded-top" />
      <Card.Body>
        <Row>
          <Col>
            <Card.Title>{props.company.id}, {props.company.name}</Card.Title>
          </Col>
          
        </Row>
        <Card.Text>Contact: {props.company.contactName}</Card.Text>
        <Card.Text>Email: {props.company.contactMail}</Card.Text>
        <Card.Text>Phone: {props.company.contactPhone}</Card.Text>
        
        <Card.Text>{props.company.presentation}</Card.Text>
        <Card.Text>{props.company.TechStack} </Card.Text>
        <Card.Text>Phone: {props.company.mentorship}</Card.Text>
        <Card.Text>Phone: {props.company.spots}</Card.Text>
        <Card.Text>Phone: {props.company.hasExJob}</Card.Text>
    </Card.Body>
    </Card>
  );
};

export default CompanyItem;
