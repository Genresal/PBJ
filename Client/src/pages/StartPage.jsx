import { Grid } from "@mui/material";
import { Box } from "@mui/system";
import { styled } from '@mui/material/styles';
import Paper from '@mui/material/Paper';
import CustomButton from "../UI/Button/CustomButton"
import GoogleIcon from '@mui/icons-material/Google';
import Separator from "../UI/Separator/Separator";
import { useContext } from "react";
import { PagesContext } from "../PagesProvider";

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

export default function StartPage() {
  const {isAuthenticated,
    setIsAuthenticated,
    userManager} = useContext(PagesContext);

  return (
    <Box sx={{
      width: '100%',
      height: '100vh',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      gap: "15em"
    }}>
      <Grid container rowSpacing={1} columnSpacing={8}>
        <Grid item xs>
            <Item>Logo Here</Item>
        </Grid>
      </Grid>
      <Grid container direction="row" rowSpacing={1}>
        <Grid item xs="auto">
            <CustomButton variant="outlined" endIcon={<GoogleIcon/>} 
            style={{
              textTransform: "none",
              borderRadius: "5%",
              color: "black",
              fontWeight: "bold",
              borderColor: "lightgray",
              paddingTop: "15px",
              paddingLeft: "15px"
            }}>
              Sign in with Google(Not Implemented)
            </CustomButton>
        </Grid>
        <Grid item>
          <Separator text="or"/>
        </Grid>
        <Grid item>
            <CustomButton onClick={() => {userManager.signinRedirect()}} style={{
              textTransform: "none",
              borderRadius: "5%",
              color: "black",
              fontWeight: "bold",
              borderColor: "lightgray",
              paddingTop: "15px",
              paddingLeft: "15px"
            }}>
              Log in
            </CustomButton>
        </Grid>
        <Grid item>
            <CustomButton onClick={() => {window.location.href = "https://localhost:7069/auth/register"; userManager.singinRedirectCallback()}} style={{
              textTransform: "none",
              borderRadius: "5%",
              color: "black",
              fontWeight: "bold",
              borderColor: "lightgray",
              paddingTop: "15px",
              paddingLeft: "15px"
            }}>
              Create an account
            </CustomButton>
        </Grid>
      </Grid>
    </Box>
  )
}
