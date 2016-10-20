using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Org.Libsdl.App;
using Urho;
using Urho.Droid;
using Urho.Shapes;
using Button = Urho.Gui.Button;

namespace TestApp.Droid2
{
    [Activity(Label = "TestApp2", Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
        MainLauncher = true, ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private SDLSurface surface;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var mLayout = new AbsoluteLayout(this);
            surface = UrhoSurface.CreateSurface(this, typeof (UrhoApp), new ApplicationOptions("Data"), true);
            mLayout.AddView(surface);
            SetContentView(mLayout);
        }

		protected override void OnResume()
		{
			UrhoSurface.OnResume();
			base.OnResume();
		}

		protected override void OnPause()
		{
			UrhoSurface.OnPause();
			base.OnPause();
		}

		public override void OnLowMemory()
		{
			UrhoSurface.OnLowMemory();
			base.OnLowMemory();
		}

		protected override void OnDestroy()
		{
			UrhoSurface.OnDestroy();
			base.OnDestroy();
		}

		public override bool DispatchKeyEvent(KeyEvent e)
		{
			if (!UrhoSurface.DispatchKeyEvent(e))
				return false;
			return base.DispatchKeyEvent(e);
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			UrhoSurface.OnWindowFocusChanged(hasFocus);
			base.OnWindowFocusChanged(hasFocus);
		}
    }
    public class UrhoApp : Urho.Application
    {
        protected override void Start()
        {
			var fish = new Button();
			fish.Texture = ResourceCache.GetTexture2D("Textures/UrhoDecal.dds");
			fish.BlendMode = BlendMode.Add;
			fish.SetSize(128, 128);
			fish.SetPosition(0, 0);
			fish.Name = "Fish";
			UI.Root.AddChild(fish);
        }
    }
}