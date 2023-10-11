import { CircularProgress, Grid } from '@mui/material';

export const Loader = () => {
  return (
    <Grid container style={{ display: 'flex', justifyContent: "center", marginTop: 50 }}>
        <CircularProgress />
    </Grid>
  )
}
