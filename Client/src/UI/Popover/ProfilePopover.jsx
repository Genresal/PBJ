import { useContext } from 'react';
import Button from '@mui/material/Button';
import Popover from '@mui/material/Popover';
import PopupState, { bindTrigger, bindPopover } from 'material-ui-popup-state';
import { PagesContext } from '../../modules/Provider/PagesProvider';

export default function ProfilePopover({buttonContent, children}) {
  const {user, userManager} = useContext(PagesContext);
  
  return (
    <PopupState variant="popover" popupId="demo-popup-popover">
      {(popupState) => (
        <div>
          <Button {...bindTrigger(popupState)} style={{textTransform: "none", color: "black"}}>
            {children}
          </Button>
          <Popover
            {...bindPopover(popupState)}
            anchorOrigin={{
              vertical: 'top',
              horizontal: 'center',
            }}
            transformOrigin={{
              vertical: 'top',
              horizontal: 'center',
            }}
          >
            <Button onClick={() => userManager.signoutRedirect()} style={{textTransform: "none", color: "black"}}>
              Log out {user.email}
            </Button>
          </Popover>
        </div>
      )}
    </PopupState>
  );
}