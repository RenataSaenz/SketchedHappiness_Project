// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TornPaperWalls"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Wind1("Wind", Float) = 10
		_TextureSample2("Texture Sample 1", 2D) = "white" {}
		_TextureSample1("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half ASEVFace : VFACE;
			float2 uv_texcoord;
		};

		uniform float _Wind1;
		uniform sampler2D _TextureSample2;
		uniform sampler2D _TextureSample1;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float temp_output_32_0 = ( ( 1.0 - step( 0.9 , v.texcoord.xy.x ) ) * step( 0.1 , v.texcoord.xy.x ) * step( 0.1 , v.texcoord.xy.y ) );
			float lerpResult39 = lerp( temp_output_32_0 , ( v.texcoord.xy.y * 0.14 ) , temp_output_32_0);
			float mulTime10 = _Time.y * 2.0;
			float lerpResult33 = lerp( lerpResult39 , sin( ( ( ( 0.5 * 6.28318548202515 ) * v.texcoord.xy.x ) + mulTime10 ) ) , lerpResult39);
			v.vertex.xyz += ( lerpResult33 * _Wind1 * float3(0,0.5,0) );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 switchResult21 = (((i.ASEVFace>0)?(float3(0,0,1)):(float3(0,0,-1))));
			o.Normal = switchResult21;
			float4 tex2DNode6 = tex2D( _TextureSample2, i.uv_texcoord );
			float4 switchResult20 = (((i.ASEVFace>0)?(tex2DNode6):(tex2DNode6)));
			o.Albedo = switchResult20.rgb;
			o.Alpha = 1;
			float4 temp_output_16_0 = ( tex2D( _TextureSample1, i.uv_texcoord ) * 10.0 );
			float4 switchResult19 = (((i.ASEVFace>0)?(temp_output_16_0):(temp_output_16_0)));
			clip( switchResult19.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;377;1920;624;1155.363;-1293.719;1;True;False
Node;AmplifyShaderEditor.TauNode;2;-2024.04,572.8912;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-2014.04,446.8912;Inherit;False;Constant;_Float5;Float 4;0;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;28;-1607.761,1119.898;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;-1586.271,1011.438;Inherit;False;Constant;_Float2;Float 0;4;0;Create;True;0;0;0;False;0;False;0.9;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1458.474,799.8541;Inherit;False;Constant;_Float4;Float 3;0;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2543.774,8.951057;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-1548.017,1669.743;Inherit;False;Constant;_Float3;Float 0;4;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1735.04,511.8912;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;29;-1324.398,1037.079;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;24;-1630.22,1471.868;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-1589.874,1358.695;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-873.0229,1526.213;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1511.04,531.8913;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;34;-1281.695,1599.306;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-758.7055,1658.211;Inherit;False;Constant;_Float6;Float 6;4;0;Create;True;0;0;0;False;0;False;0.14;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;22;-1316.24,1369.555;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;30;-1060.821,1151.262;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-1311.941,766.9835;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1104.822,584.0113;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-635.5725,1097.247;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-500.7976,1541.303;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;39;-88.32071,1205.327;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;15;-899.589,578.6614;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-2072.271,-137.6518;Inherit;True;Property;_TextureSample1;Texture Sample 0;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-2026.286,131.4608;Inherit;False;Constant;_Float1;Float 0;3;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;17;303.6041,973.7256;Inherit;False;Constant;_Vector4;Vector 3;1;0;Create;True;0;0;0;False;0;False;0,0.5,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1749.711,-35.74298;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;18;-787.1315,9.088803;Inherit;False;Constant;_Vector1;Vector 0;4;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;14;117.9068,747.6745;Inherit;False;Property;_Wind1;Wind;1;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;33;-129.3787,713.6376;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;13;-789.1257,156.5326;Inherit;False;Constant;_Vector2;Vector 1;4;0;Create;True;0;0;0;False;0;False;0,0,-1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;6;-2147.417,-344.375;Inherit;True;Property;_TextureSample2;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;a37c6552b9d628b4a98737d62b1cc40c;a37c6552b9d628b4a98737d62b1cc40c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwitchByFaceNode;21;-599.1337,77.33563;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwitchByFaceNode;20;-1050.126,-558.9248;Inherit;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;476.349,828.3741;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwitchByFaceNode;19;-1172.936,135.2569;Inherit;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;TornPaperWalls;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;1;0
WireConnection;8;1;2;0
WireConnection;29;0;27;0
WireConnection;29;1;28;1
WireConnection;11;0;8;0
WireConnection;11;1;3;1
WireConnection;34;0;35;0
WireConnection;34;1;24;2
WireConnection;22;0;23;0
WireConnection;22;1;24;1
WireConnection;30;0;29;0
WireConnection;10;0;9;0
WireConnection;12;0;11;0
WireConnection;12;1;10;0
WireConnection;32;0;30;0
WireConnection;32;1;22;0
WireConnection;32;2;34;0
WireConnection;37;0;36;2
WireConnection;37;1;38;0
WireConnection;39;0;32;0
WireConnection;39;1;37;0
WireConnection;39;2;32;0
WireConnection;15;0;12;0
WireConnection;5;1;3;0
WireConnection;16;0;5;0
WireConnection;16;1;4;0
WireConnection;33;0;39;0
WireConnection;33;1;15;0
WireConnection;33;2;39;0
WireConnection;6;1;3;0
WireConnection;21;0;18;0
WireConnection;21;1;13;0
WireConnection;20;0;6;0
WireConnection;20;1;6;0
WireConnection;7;0;33;0
WireConnection;7;1;14;0
WireConnection;7;2;17;0
WireConnection;19;0;16;0
WireConnection;19;1;16;0
WireConnection;0;0;20;0
WireConnection;0;1;21;0
WireConnection;0;10;19;0
WireConnection;0;11;7;0
ASEEND*/
//CHKSM=8A58E0FBBC2AB14AB5DF7D82B50E30AB58BA2F19