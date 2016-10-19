using System.Diagnostics;
using Urho;
using Urho.Gui;
using Urho.Shapes;

namespace TestApp
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await UrhoSurface.Show<UrhoApp>(new ApplicationOptions("Data"));
        }

        private class UrhoApp : Urho.Application
        {
            private Node _boxNode;
            private Scene _scene;
            private Camera _camera;

            protected override void Start()
            {
                _scene = new Scene();
                _scene.CreateComponent<Octree>();
                // Box
                _boxNode = _scene.CreateChild(name: "box");
                _boxNode.Position = new Vector3(0, 0, 5);
                _boxNode.Rotation = new Quaternion(60, 0, 30);
                _boxNode.SetScale(1f);        
                var box = _boxNode.CreateComponent<Box>();
                
                // Light
                Node lightNode = _scene.CreateChild(name: "light");
                lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
                lightNode.CreateComponent<Light>();
                
                // Camera
                Node cameraNode = _scene.CreateChild(name: "camera");
                _camera = cameraNode.CreateComponent<Camera>();

                var text = ResourceCache.GetTexture2D("my_texture.bmp");
                Debug.WriteLine(
                    text == null ? "### Did not find the texture ###" : "### Found the texture ###");

                // Viewport
                Renderer.SetViewport(0, new Viewport(Context, _scene, _camera, null));

                var image = new BorderImage
                {
                    Texture = text,
                    BlendMode = BlendMode.Add,
                    Size = new IntVector2(200, 200),
                    Position = new IntVector2(0, 0),
                };
                UI.Root.AddChild(image);
            }
        }
    }
}
