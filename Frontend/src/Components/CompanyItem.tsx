import React, { useState } from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import ConnectToItem from '../Components/ConnectToItem';
import { CompanyModel } from '../Models/CompanyModel';

interface CompanyItemProps {
  company: CompanyModel;
}

const CompanyItem: React.FC<CompanyItemProps> = (props) => {
  const [isLiked, setIsLiked] = useState(false);

  const handleLikeClick = () => {
    setIsLiked(!isLiked);
  };

  return (
    <Card className="mb-4 border bg-light rounded shadow overflow-hidden" data-testid="card-id">
      {/* Image overlay */}
      <div
        className="position-relative"
        style={{
          height: '20vh',
        }}
      >
        <div className="rounded shadow-sm position-absolute w-100 h-100 d-flex align-items-left"
        style={{height: '20vh'}}>
        <Card.Title className="text-black text-center" data-testid="name-id"><h1>{props.company.name}</h1></Card.Title>
      </div>
        <img
          src={props.company.imgURL}
          alt={props.company.name}
          className="w-100 h-100 object-fit-cover"
        />
        
      </div>

      <Card.Body>
        <div className="mb-3 text-black">
          <p>{props.company.presentation}</p>
          <p className="font-weight-bold">{props.company.TechStack}</p>
        </div>

        <Row className="text-black">
          <Col>
            <p>Contact: {props.company.contactName}</p>
            <p>Email: {props.company.contactMail}</p>
            <p>Phone: {props.company.contactPhone}</p>
          </Col>
        </Row>

        <ConnectToItem onLike={handleLikeClick} isLiked={isLiked} />
      </Card.Body>
    </Card>
  );
};

export default CompanyItem;