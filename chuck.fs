/*{
	"DESCRIPTION": "chuck close",
	"CREDIT": "shadertoy.com/user/netgrind",
	"CATEGORIES": [
		"mind expansion"
	],
	"INPUTS": [
		{
			"NAME": "iChannel0",
			"TYPE": "image"
		},
      	],
}*/

vec3 iResolution = vec3(RENDERSIZE, 1.);
float iTime = TIME;


#define size 32
#define sampleStep 8

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 m =  mod(fragCoord.xy, vec2(size));
	vec2 uv = fragCoord.xy-m;
    vec4[int(pow(float(size/sampleStep), 2.))] c;// = vec4(0.);//texture(iChannel0, uv);
    int x = 0;
    for(int i = 0; i<size; i+=sampleStep){
        for(int j = 0; j<size; j+=sampleStep){
            c[x]=texture(iChannel0, (uv+vec2(i,j))/iResolution.xy);
            x++;
        }
    }
    float t = iTime*3.;
    float f = length(m-float(size)*.5)*.666-t;
    int i = int(mod(f, float(x)));
    int j = int(mod(f+1., float(x)));
    vec4 o =mix( c[i], c[j], mod(f, 1.));
     
	fragColor = o;
}

void main(void) {
    mainImage(gl_FragColor, gl_FragCoord.xy);
}