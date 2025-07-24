import { Spinner } from "reactstrap";

export const FullScreenSpinner = () => {
    return (
        <div style={overlayStyle}>
            <Spinner color="primary" style={spinnerStyle}/>
        </div>
    );
};

const spinnerStyle = {
    width: "150px",
    height: "150px",
    fontSize: "40px",
};

const overlayStyle = {
    position: "fixed",
    top: 0,
    left: 0,
    width: "100vw",
    height: "100vh",
    backgroundColor: "rgba(255, 255, 255, 0.6)", 
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    zIndex: 1050,
}