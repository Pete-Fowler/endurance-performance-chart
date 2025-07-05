import { Row, Col } from "reactstrap";

import styles from "./FormLegend.module.css";

export function FormLegend() {
    const zones = [
        { label: "Transition", color: "#ffe066" }, // yellow
        { label: "Fresh", color: "#74c0fc" }, // blue
        { label: "Neutral", color: "#adb5bd" }, // gray
        { label: "Optimal", color: "#51cf66" }, // green
        { label: "High Risk", color: "#fa5252" }, // red
    ];
    
    return (
        <Row>
            <Col className={styles.formLegend}>
                {zones.map((zone) => (
                    <div
                        key={zone.label}
                        style={{
                            color: zone.color,
                            fontWeight: "bold",
                        }}
                    >
                        {zone.label}
                    </div>
                ))}
            </Col>
        </Row>
    );
}
