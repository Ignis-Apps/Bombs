Shader "Unlit/SimpleVertexShader"
{
    SubShader {
        
        Pass {	
  
         ZWrite ON
         Blend SrcAlpha OneMinusSrcAlpha 
             
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
  
         struct vertexInput {
            float4 vertex : POSITION;
            float4 color : COLOR;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 color : COLOR;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
             //output.tex = input.texcoord;
            output.pos = UnityObjectToClipPos(input.vertex);
            output.color = input.color;
            return output;
         }

         float4 frag(vertexOutput input) : COLOR
         {
            return input.color;    
         }
 
         ENDCG
      }
    
}
}
