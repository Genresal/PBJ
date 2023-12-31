import { Button, Grid, Divider } from "@mui/material";
import { Box } from "@mui/system";
import GoogleIcon from '@mui/icons-material/Google';
import { useContext } from "react";
import { PagesContext } from "../modules/Provider/PagesProvider";
import { Item } from "../UI/Item/Item";

export default function StartPage() {
  const { userManager } = useContext(PagesContext);

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
      <Grid container>
        <Grid item xs="auto">
            <Button variant="outlined" endIcon={<GoogleIcon/>} 
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
            </Button>
        </Grid>
        <Divider >
          or
        </Divider>
        <Grid item>
            <Button onClick={() => {userManager.signinRedirect()}} style={{
              textTransform: "none",
              borderRadius: "5%",
              color: "black",
              fontWeight: "bold",
              borderColor: "lightgray",
              paddingTop: "15px",
              paddingLeft: "15px"
            }}>
              Log in
            </Button>
        </Grid>
      </Grid>
    </Box>
  )
}
