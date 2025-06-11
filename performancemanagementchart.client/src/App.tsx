import { Col, Container, Row } from 'reactstrap';

import PerformanceManagementChart from './PerformanceManagementChart/PerformanceManagementChart';
import styles from './App.module.css';

export default function App() {
   
    return (
        <Container className={styles.container}>
            <Row>
                <Col className={styles.chartBox}>
                    <h1>Performance Management Chart</h1>
                    <PerformanceManagementChart />
                </Col>
            </Row>
        </Container>
    );
}
