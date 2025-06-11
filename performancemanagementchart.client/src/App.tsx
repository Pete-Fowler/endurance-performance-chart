import { Container } from 'reactstrap';

import PerformanceManagementChart from './PerformanceManagementChart/PerformanceManagementChart';
import styles from './App.module.css';

export default function App() {
   
    return <Container className={styles.container}>
        <h1>Performance Management Chart</h1>
        <PerformanceManagementChart />
    </Container>
}
