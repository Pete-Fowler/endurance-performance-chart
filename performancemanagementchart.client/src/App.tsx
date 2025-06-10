import './App.css';
import { Container } from 'reactstrap';

import PerformanceManagementChart from './PerformanceManagementChart/PerformanceManagementChart';


export default function App() {
   
    return <Container>
        <h1>Performance Management Chart</h1>
        <PerformanceManagementChart />
    </Container>
}
