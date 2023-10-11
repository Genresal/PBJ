import {Avatar, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Grid} from "@mui/material";
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import HomeOutlinedIcon from '@mui/icons-material/HomeOutlined';
import AccountBoxOutlinedIcon from '@mui/icons-material/AccountBoxOutlined';
import PeopleAltOutlinedIcon from '@mui/icons-material/PeopleAltOutlined';
import { Link } from "react-router-dom";
import { PagesContext } from "../../modules/Provider/PagesProvider";
import { useContext } from "react";
import ProfilePopover from "../Popover/ProfilePopover";

const NavMenu = () => {
    const {user} = useContext(PagesContext)

    return (
        <div>
            <Drawer variant="permanent" anchor="left" 
            sx={{
            width: 300,
             '& .MuiDrawer-paper': {
                width: 250,
                boxSizing: 'border-box',
            },}}>
                <List>
                    <Link to="/" style={{color: "black"}}>
                        <ListItem>
                            <ListItemButton style={{borderRadius: 30}}>
                                <ListItemIcon>
                                    <HomeOutlinedIcon />
                                </ListItemIcon>
                                <ListItemText primary="Home"/>
                            </ListItemButton>
                        </ListItem>
                    </Link>
                    <Link to="/search" style={{color: "black"}}>
                        <ListItem>
                            <ListItemButton style={{borderRadius: 30}}>
                                <ListItemIcon>
                                    <SearchOutlinedIcon />
                                </ListItemIcon>
                                <ListItemText primary="Explore"/>
                            </ListItemButton>
                        </ListItem>
                    </Link>
                    <Link to="/followers" style={{color: "black"}}>
                        <ListItem>
                            <ListItemButton style={{borderRadius: 30}}>
                                <ListItemIcon>
                                    <PeopleAltOutlinedIcon />
                                </ListItemIcon>
                                <ListItemText primary="Followers"/>
                            </ListItemButton>
                        </ListItem>
                    </Link>
                    <Link to="/followings" style={{color: "black"}}>
                        <ListItem>
                            <ListItemButton style={{borderRadius: 30}}>
                                <ListItemIcon>
                                    <HomeOutlinedIcon />
                                </ListItemIcon>
                                <ListItemText primary="Followings"/>
                            </ListItemButton>
                        </ListItem>
                    </Link>
                    <Link to="/profile" style={{color: "black"}}>
                        <ListItem>
                            <ListItemButton style={{borderRadius: 30}}>
                                <ListItemIcon>
                                    <AccountBoxOutlinedIcon />
                                </ListItemIcon>
                                <ListItemText primary="Profile"/>
                            </ListItemButton>
                        </ListItem>
                    </Link>
                    <ListItem style={{position: "fixed", bottom: "10px", width: "250px"}}>
                        <ProfilePopover>
                            <ListItemButton style={{borderRadius: 30}}>
                                <ListItemIcon>
                                    <Avatar>A</Avatar>
                                </ListItemIcon>
                                <ListItemText>
                                    <Grid container direction="column" justifyContent="left">
                                        <Grid item style={{fontWeight: "bold"}}>{user.name}</Grid>
                                        <Grid item style={{fontSize: 13}}>{user.email}</Grid>
                                    </Grid>
                                </ListItemText>
                            </ListItemButton>
                        </ProfilePopover>
                    </ListItem>
                </List>
            </Drawer>
        </div>

    );
};

export default NavMenu;