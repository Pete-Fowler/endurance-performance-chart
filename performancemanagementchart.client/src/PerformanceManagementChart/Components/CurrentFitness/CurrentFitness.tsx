import { Row, Col } from "reactstrap";
import type { IFitnessData } from "../../Interfaces";
import styles from "./CurrentFitness.module.css";
import { colors } from "../../Colors";

interface IProps {
    currentFitness: IFitnessData;
}

export default function CurrentFitness({currentFitness}: IProps) 
{
    return (
        <Col className={styles.fitnessBox}>
            <div style={{ color: colors.gray }}>
                Form: {currentFitness.form}
            </div>
            <div style={{ color: colors.blue }}>
                Fitness: {currentFitness.fitness}
            </div>
            <div style={{ color: colors.purple }}>
                Fatigue: {currentFitness.fatigue}
            </div>
        </Col>
    );
}