import { useEffect, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { getAllCompanies } from "../Services/CompanyServices";
import CompanyItem from "../Components/CompanyItem";
import { CompanyModel } from "../Models/CompanyModel";

export function Companies() {
  const [companies, setCompanies] = useState<CompanyModel[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const companiesData = await getAllCompanies();
        setCompanies(companiesData);
      } catch (error) {
        console.error("Error fetching companies:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <Container>
      <h1 className="mulish-font">Companies</h1>
      {companies.length > 0 ? (
        <Row md={2} xs={1} lg={3} className="g-3">
          {companies.map((item) => (
            <Col key={item.id} style={{ marginBottom: '16px' }}>
              <CompanyItem company={item} data-testid="company-id" />
            </Col>
          ))}
        </Row>
      ) : (
        <p>No companies to list.</p>
      )}
    </Container>
  );
}
