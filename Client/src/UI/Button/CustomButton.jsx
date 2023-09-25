import {Button} from "@mui/material"

const CustomButton = ({children, variant, ...props}) => {
    return (
        <Button variant={variant} {...props}>
            {children}
        </Button>
    );
};

export default CustomButton;