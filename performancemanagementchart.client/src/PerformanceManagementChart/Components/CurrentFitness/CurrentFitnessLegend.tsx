import { Col } from "reactstrap";
import type { IFitnessData } from "../../Interfaces";
import styles from "./CurrentFitnessLegend.module.css";
import { colors } from "../../Colors";

interface IProps {
    currentFitness: IFitnessData;
}

export default function CurrentFitnessLegend({currentFitness}: IProps) 
{
    return (
        <Col className={styles.fitnessBox}>
            <div style={{ color: colors.gray, fontWeight: "bold" }}> 
                Form: {currentFitness.Form}
            </div>
            <div style={{ color: colors.blue, fontWeight: "bold" }}>
                Fitness: {currentFitness.Fitness}
            </div>
            <div style={{ color: colors.purple, fontWeight: "bold" }}>
                Fatigue: {currentFitness.Fatigue}
            </div>
        </Col>
    );
}