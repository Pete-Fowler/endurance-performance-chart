import { Col } from "reactstrap";
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
                Form: {currentFitness.Form}
            </div>
            <div style={{ color: colors.blue }}>
                Fitness: {currentFitness.Fitness}
            </div>
            <div style={{ color: colors.purple }}>
                Fatigue: {currentFitness.Fatigue}
            </div>
        </Col>
    );
}