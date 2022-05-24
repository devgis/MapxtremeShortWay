using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using MapInfo.Windows.Dialogs;
using MapInfo.Data;
using MapInfo.Mapping;
using MapInfo.Tools;
using MapInfo.Engine;
using System.Text;
using MapInfo.Geometry;
using System.Diagnostics;

namespace Devgis.ShortWay
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ShortWay : System.Windows.Forms.Form
	{
        //DataTable dtRoad;
        Hashtable hashAllMap=new Hashtable();
        Hashtable hashMapName = new Hashtable();
        #region 系统定义的
        private MapInfo.Windows.Controls.MapControl mapControl1;
        private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton ZoomIn;
		private System.Windows.Forms.ToolBarButton ZoomOut;
		private System.Windows.Forms.ToolBarButton Pan;
		private System.Windows.Forms.ToolBarButton EntireView;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton DestroyRoad;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
        private Graph graph;
        private Hashtable hashGraph;
		private int nodeCount;
		private Dijkstra dij;
        private int FromNode=30;
		private System.Windows.Forms.ToolBarButton LayerControl;
		private int ToNode;
        private Button btSearch;
        private ComboBox cbStart;
        private ComboBox cbEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Panel panel1;
        private Panel panel2;
        public ShortWay()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortWay));
            this.mapControl1 = new MapInfo.Windows.Controls.MapControl();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.LayerControl = new System.Windows.Forms.ToolBarButton();
            this.ZoomIn = new System.Windows.Forms.ToolBarButton();
            this.ZoomOut = new System.Windows.Forms.ToolBarButton();
            this.Pan = new System.Windows.Forms.ToolBarButton();
            this.EntireView = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.DestroyRoad = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btSearch = new System.Windows.Forms.Button();
            this.cbStart = new System.Windows.Forms.ComboBox();
            this.cbEnd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.IgnoreLostFocusEvent = false;
            this.mapControl1.Location = new System.Drawing.Point(0, 28);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(815, 485);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.Text = "mapControl1";
            this.mapControl1.Tools.LeftButtonTool = null;
            this.mapControl1.Tools.MiddleButtonTool = null;
            this.mapControl1.Tools.RightButtonTool = null;
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.LayerControl,
            this.ZoomIn,
            this.ZoomOut,
            this.Pan,
            this.EntireView,
            this.toolBarButton1,
            this.DestroyRoad});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(815, 28);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // LayerControl
            // 
            this.LayerControl.ImageIndex = 1;
            this.LayerControl.Name = "LayerControl";
            // 
            // ZoomIn
            // 
            this.ZoomIn.ImageIndex = 2;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.ToolTipText = "放大";
            // 
            // ZoomOut
            // 
            this.ZoomOut.ImageIndex = 3;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.ToolTipText = "缩小";
            // 
            // Pan
            // 
            this.Pan.ImageIndex = 4;
            this.Pan.Name = "Pan";
            this.Pan.ToolTipText = "平移";
            // 
            // EntireView
            // 
            this.EntireView.ImageIndex = 5;
            this.EntireView.Name = "EntireView";
            this.EntireView.ToolTipText = "全图";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // DestroyRoad
            // 
            this.DestroyRoad.ImageIndex = 7;
            this.DestroyRoad.Name = "DestroyRoad";
            this.DestroyRoad.ToolTipText = "损毁道路";
            this.DestroyRoad.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(449, 2);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 2;
            this.btSearch.Text = "最优搜索";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // cbStart
            // 
            this.cbStart.FormattingEnabled = true;
            this.cbStart.Location = new System.Drawing.Point(218, 4);
            this.cbStart.Name = "cbStart";
            this.cbStart.Size = new System.Drawing.Size(84, 20);
            this.cbStart.TabIndex = 3;
            // 
            // cbEnd
            // 
            this.cbEnd.FormattingEnabled = true;
            this.cbEnd.Location = new System.Drawing.Point(348, 4);
            this.cbEnd.Name = "cbEnd";
            this.cbEnd.Size = new System.Drawing.Size(84, 20);
            this.cbEnd.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "起点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "终点";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 485);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(301, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 13;
            // 
            // ShortWay
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(815, 513);
            this.Controls.Add(this.mapControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbEnd);
            this.Controls.Add(this.cbStart);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.toolBar1);
            this.Name = "ShortWay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "路径分析Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShortWay_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion 
        
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == LayerControl)
			{
                layerDlg();
			}
			if(e.Button == ZoomIn)
			{
				this.mapControl1.Tools.LeftButtonTool = "ZoomIn";
			}
			if(e.Button == ZoomOut)
			{
				this.mapControl1.Tools.LeftButtonTool = "ZoomOut";
			}
			if(e.Button == Pan)
			{
				this.mapControl1.Tools.LeftButtonTool = "Pan";
			}
			if(e.Button == EntireView)
			{
				MapInfo.Mapping.IMapLayerFilter iml = MapInfo.Mapping.MapLayerFilterFactory.FilterByType(typeof(MapInfo.Mapping.FeatureLayer));
				MapInfo.Mapping.MapLayerEnumerator mle = this.mapControl1.Map.Layers.GetMapLayerEnumerator(iml);
				this.mapControl1.Map.SetView(mle);
			}
		}

		private void layerDlg()
		{
			LayerControlDlg lcDlg = new LayerControlDlg();
			lcDlg.Map = mapControl1.Map;
			lcDlg.ShowDialog(this);
		}

        private void ShortWay_Load(object sender, EventArgs e)
        {
            #region 加载道路
            string MapPath = Path.Combine(Application.StartupPath, @"Map\map.mws");
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MapPath);
            mapControl1.Map.Load(mwsLoader);

            FeatureLayer flNode = mapControl1.Map.Layers["Node"] as FeatureLayer;
            flNode.Enabled = false;
            #endregion

            Table tblNode = Session.Current.Catalog.GetTable("Node");
            System.Collections.Generic.List<int> listNode1 = new System.Collections.Generic.List<int>();
            System.Collections.Generic.List<int> listNode2= new System.Collections.Generic.List<int>();
            foreach (Feature feature in (tblNode as MapInfo.Data.ITableFeatureCollection))
            {
                int iKey = Convert.ToInt32(feature["Name"]);
                listNode1.Add(iKey);
                listNode2.Add(iKey);
            }
            cbStart.DataSource = listNode1;
            cbEnd.DataSource = listNode2;
        }
        StringBuilder sbText;
        private void btSearch_Click(object sender, EventArgs e)
        {
            if (cbStart.Text.Equals(cbEnd.Text))
            {
                PublicDim.ShowErrorMessage("起点终点不能相同！");
                return;
            }

            #region 清除上次图层
            Session.Current.Selections.DefaultSelection.Clear();
            #endregion 
            #region 获取起点终点
            if (cbStart.Text.Trim() == "" || cbEnd.Text.Trim() == "")
            {
                PublicDim.ShowErrorMessage("请选择起点和终点！");
                return;
            }
            else
            {
                try
                {
                    FromNode = Convert.ToInt32(cbStart.Text);
                    ToNode = Convert.ToInt32(cbEnd.Text);
                }
                catch
                {
                    PublicDim.ShowErrorMessage("请选择起点和终点！");
                    return;
                }

            }
            #endregion
            #region 创建图

            Table tableRoad = Session.Current.Catalog.GetTable("Road");
            System.Collections.Generic.List<RoadInfo> lstRoadInfo = new System.Collections.Generic.List<RoadInfo>();
            foreach (Feature feature in (tableRoad as MapInfo.Data.ITableFeatureCollection))
            {
                RoadInfo rInfo = new RoadInfo();
                //rInfo.Key= Convert.ToInt32(feature["Name"]);
                rInfo.FNode = Convert.ToInt32(feature["FNODE"]);
                rInfo.TNode = Convert.ToInt32(feature["TNODE"]);
                rInfo.Length = Convert.ToDouble((feature.Geometry as MapInfo.Geometry.MultiCurve).Length(MapInfo.Geometry.DistanceUnit.Meter));
                lstRoadInfo.Add(rInfo);
            }
            int iCount = 0;
            Table tableNode = Session.Current.Catalog.GetTable("Node");
            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchAll();
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableNode, si);
            iCount = ifs.Count;

            graph = new Graph(lstRoadInfo, iCount);
            graph.ConstructGraph();

            this.hashGraph = graph.HashGraph;
            graph.nodeCount = iCount + 1;
            this.nodeCount = graph.NodeCount;
            

            dij = new Dijkstra(hashGraph, FromNode, nodeCount);
            dij.GenerateMinPath();

            #endregion
            #region 查找
            bool bFind = true;
            ArrayList MinPath = dij.GetMinPath(ToNode);
            if (MinPath.Count <= 0)
            {
                PublicDim.ShowErrorMessage("没有可通达的最短路径！");
                return;
            }
            sbText = new StringBuilder();
            for (int i = 0; i < MinPath.Count - 1; i++)
            {
                int FromNodeID = Convert.ToInt32(MinPath[i]);
                int ToNodeID = Convert.ToInt32(MinPath[i + 1]);
                
                SelectRoad(FromNodeID, ToNodeID);
            }

            if (!bFind)
            {
                PublicDim.ShowErrorMessage("没有可通达的最短路径！");
            }
            else
            {
                String SPath=Application.StartupPath + "\\Data\\";
                if (!Directory.Exists(SPath))
                {
                    Directory.CreateDirectory(SPath);
                }
                String fPaths = SPath + Guid.NewGuid().ToString() + ".txt";
                File.WriteAllText(fPaths, sbText.ToString());
                System.Diagnostics.Process.Start("notepad.exe", fPaths);
            }

            #endregion
        }

        #region 检测两个两点之间通路的数量，路径分析用
        Hashtable hsTemp = new Hashtable();
        private float getLength(int iStartNodeId, int iEndNodeId)
        {
            hsTemp = (Hashtable)hashAllMap[iStartNodeId];
            if (!hsTemp.ContainsKey(iEndNodeId))
            {
                return 0;
            }
            else
                return (float)hsTemp[iEndNodeId];//dLength;
        }
        #endregion

        /// <summary>
        /// 根据起点终点选择线段
        /// </summary>
        /// <param name="FromNodeID"></param>
        /// <param name="ToNodeID"></param>
        private void SelectRoad(int FromNodeID, int ToNodeID)
        {
            String sWhere = "(FNODE='" + FromNodeID + "' And TNODE='" + ToNodeID + "')";
            Table tableRoad = Session.Current.Catalog.GetTable("Road");
            SearchInfo siWhere = MapInfo.Data.SearchInfoFactory.SearchWhere(sWhere);
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tableRoad, siWhere);
            Session.Current.Selections.DefaultSelection.Add(ifs);
            foreach (Feature f in ifs)
            {
                foreach (Curve c in (f.Geometry as MultiCurve))
                {
                    foreach (DPoint dp in c.SamplePoints())
                    {
                        sbText.AppendLine(String.Format("{0},{1}", dp.x, dp.y));
                    }
                }
            }

            System.Threading.Thread.Sleep(50);
        }
	}
}
