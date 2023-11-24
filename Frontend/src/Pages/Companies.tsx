import { Row, Col } from "react-bootstrap";
import companies from "../Data/companies.json";
import CompanyItem from "../Components/CompanyItem";


export function Companies(){
    return (
        <>
        <h1>Companies</h1>
        <Row md={2} xs={1} lg={3} className="g-3">
            {companies.map(item => (
                <Col key={item.id}><CompanyItem company={item} key={item.id} data-testid="company-id" /></Col>

            ))}
        </Row>
        </>
    )
}