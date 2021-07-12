using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

public class SnowballScript : Script
{
  private ScriptSettings Config;
  private Keys PickupSnowballKey;

  public SnowballScript()
  {
    this.Tick += new EventHandler(this.OnTick);
    this.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this.LoadIniFile("scripts//SnowballMod.ini");
  }

  private void LoadIniFile(string iniName)
  {
    try
    {
      this.Config = ScriptSettings.Load(iniName);
      this.PickupSnowballKey = (Keys) this.Config.GetValue<Keys>("Configurations", "PickSnowballUp", (M0) 121);
    }
    catch (Exception ex)
    {
      UI.Notify("~r~Error~w~: SnowballMod.ini Failed To Load.");
    }
  }

  private void OnTick(object sender, EventArgs r)
  {
  }

  private void OnKeyUp(object sender, KeyEventArgs e)
  {
  }

  private void PlayAnimation(string animation, string animation2)
  {
    Function.Call((Hash) -3189321952077349130L, new InputArgument[1]
    {
      InputArgument.op_Implicit(animation)
    });
    while (true)
    {
      if (Function.Call<bool>((Hash) -3444786327252301684L, new InputArgument[1]
      {
        InputArgument.op_Implicit(animation)
      }) == null)
        Script.Wait(0);
      else
        break;
    }
    Function.Call((Hash) -1565002832890405996L, new InputArgument[11]
    {
      InputArgument.op_Implicit(Game.Player.Character),
      InputArgument.op_Implicit(animation),
      InputArgument.op_Implicit(animation2),
      InputArgument.op_Implicit(8.0),
      InputArgument.op_Implicit(0.0),
      InputArgument.op_Implicit(-1),
      InputArgument.op_Implicit(0),
      InputArgument.op_Implicit(0),
      InputArgument.op_Implicit(0),
      InputArgument.op_Implicit(0),
      InputArgument.op_Implicit(0)
    });
  }

  private void OnKeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == this.PickupSnowballKey && Game.Player.Character.IsInVehicle() || e.KeyCode != this.PickupSnowballKey)
      return;
    this.PlayAnimation("anim@mp_snowball", "pickup_snowball");
    Game.Player.Character.Weapons.Give((WeaponHash) 126349499, 0, true, true);
    Function.Call((Hash) 8714538174022443552L, new InputArgument[3]
    {
      InputArgument.op_Implicit(Game.Player.Character),
      InputArgument.op_Implicit(126349499),
      InputArgument.op_Implicit(1)
    });
    Script.Wait(2500);
    Game.Player.Character.Task.ClearAllImmediately();
  }
}
